package com.leon2025.kzloggerplugin;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.os.Handler;
import android.os.Looper;

import java.io.*;
import java.nio.charset.StandardCharsets;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

public class KzLogger {
    private static final String LOG_FILE_NAME = "unity_logs.txt";
    private static final Object fileLock = new Object();
    private static final SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss.SSS", Locale.US);
    private static final Handler mainHandler = new Handler(Looper.getMainLooper());

    private static KzLogger instance;
    private Context context;

    private KzLogger(Context context) {
        this.context = context.getApplicationContext();
    }

    public static synchronized KzLogger getInstance(Context context) {
        if (instance == null) {
            instance = new KzLogger(context);
        }
        return instance;
    }

    public void recordLog(String level, String message, String stack) {
        if (context == null) return;
        String ts = sdf.format(new Date());
        StringBuilder sb = new StringBuilder();
        sb.append("[").append(ts).append("] ");
        sb.append(level).append(": ").append(message);
        if (stack != null && !stack.isEmpty()) {
            sb.append("\n").append(stack);
        }
        sb.append("\n\n");

        synchronized (fileLock) {
            File file = new File(context.getFilesDir(), LOG_FILE_NAME);
            try (FileOutputStream fos = new FileOutputStream(file, true)) {
                fos.write(sb.toString().getBytes(StandardCharsets.UTF_8));
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    public String getAllLogs() {
        if (context == null) return "";
        File file = new File(context.getFilesDir(), LOG_FILE_NAME);
        if (!file.exists()) return "";

        StringBuilder content = new StringBuilder();
        synchronized (fileLock) {
            try (BufferedReader br = new BufferedReader(
                    new InputStreamReader(new FileInputStream(file), StandardCharsets.UTF_8))) {
                String line;
                while ((line = br.readLine()) != null) {
                    content.append(line).append("\n");
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return content.toString();
    }

    public void requestClearLogs(Activity activity) {
        if (activity == null) return;
        mainHandler.post(() -> new AlertDialog.Builder(activity)
                .setTitle("Borrar logs")
                .setMessage("¿Estás seguro que querés borrar todos los logs?")
                .setPositiveButton("Borrar", (dialog, which) -> {
                    synchronized (fileLock) {
                        File file = new File(context.getFilesDir(), LOG_FILE_NAME);
                        if (file.exists()) {
                            file.delete();
                        }
                    }
                })
                .setNegativeButton("Cancelar", (dialog, which) -> dialog.dismiss())
                .setCancelable(true)
                .show());
    }
}
