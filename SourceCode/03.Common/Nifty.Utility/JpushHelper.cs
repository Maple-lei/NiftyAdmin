using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cn.jpush.api;
using cn.jpush.api.push.mode;
using System.Timers;
using System.Web.Mvc;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WZ.Common
{
    public class JpushHelper
    {
        private static String appKey = "2617b2fec9b11a5659e6133c";
        private static String masterSecret = "4016ed9d4fcc0e56ff8a14b8";

        private static JPushClient jpush = new JPushClient(appKey, masterSecret);
        System.Timers.Timer timer = new System.Timers.Timer();

        public JpushHelper()
        {

        }

        public void SendNotification()
        {
            jpush.SendPush(PushObject_alias_tags_alert());
        }

        public PushPayload PushObject_alias_tags_alert()
        {
            PushPayload pushPayload_alias_tags = new PushPayload();
            pushPayload_alias_tags.platform = Platform.android_ios();
            pushPayload_alias_tags.audience = Audience.all();

            Random rd = new Random();
            PushEntity pushEntity = new PushEntity();
            pushEntity.WarnCount = rd.Next(1, 10);
            pushEntity.Content = "有" + pushEntity.WarnCount.ToString() + "条报警";

            pushPayload_alias_tags.message = Message.content(ObjectToJson(pushEntity)).setTitle("设备报警").setContentType("报警");

            return pushPayload_alias_tags;
        }

        public string ObjectToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        }
    }

    public class PushEntity
    {
        public int WarnCount { get; set; }

        public string Content { get; set; }
    }
}
