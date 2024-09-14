package com.example.myhome;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.telephony.SmsMessage;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.PopupWindow;
import android.widget.Toast;
import android.widget.LinearLayout.LayoutParams;

public class ReceiveAlert extends BroadcastReceiver {
    private SmsMessage[] alerts = null;
    private String alertFrom = "";
    private String alertBody = "";
    @Override
    public void onReceive(Context context, Intent intent) {
        //Toast.makeText(context, "House breaking alert..", Toast.LENGTH_LONG).show();
        if(intent.getAction().equals("android.provider.Telephony.SMS_RECEIVED")) {
            Bundle bundle = intent.getExtras();
            if (bundle != null) {
                try {
                    Object[] pdus = (Object[]) bundle.get("pdus");
                    alerts = new SmsMessage[pdus.length];
                    for (int i = 0; i < alerts.length; i++) {
                        alerts[i] = SmsMessage.createFromPdu((byte[]) pdus[i]);
                        alertFrom = alerts[i].getOriginatingAddress();
                        alertBody = alerts[i].getMessageBody();
                        //Toast.makeText(context, alertFrom + ":" + alertBody, Toast.LENGTH_LONG).show();
                    }
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
    }

    public String getAlertBody() {
        return alertBody;
    }

    public String getAlertFrom() {
        return alertFrom;
    }
}
