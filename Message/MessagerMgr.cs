using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.JackCheng.GameRemind
{
    /// <summary>
    /// 消息管理器 
    /// </summary>
    public class MessagerMgr
    {
        private Dictionary<string, List<MessageItem>> handles = new Dictionary<string, List<MessageItem>>();

        public void addMessageLister(object reciver,string type, MsgHandler handle)
        {

            if(reciver == null || type == null || handle == null) throw new Exception("传入的参数含有null值");
            

            List<MessageItem> items; MessageItem item;
            
            if (handles.ContainsKey(type) == false) {

                item = new MessageItem();
                item.reciver = reciver;
                item.handle = handle;
                items = new List<MessageItem>(){item};
                handles[type] = items;
              
              return;
            }

            items = handles[type];


            //接收者已经注册过该消息;
            foreach (MessageItem it in items) {
                if (it.reciver == reciver)
                {
                    it.handle += handle;
                    return;
                }
            }

            //没有此接收者；
            item = new MessageItem();
            item.reciver = reciver;
            item.handle  = handle;
            items = handles[type];
            items.Add(item);

        }

        public void delMessageLister(object reciver, string type, MsgHandler handle)
        {

            if (reciver == null || type == null || handle == null) throw new Exception("传入的参数含有null值");

            if (handles.ContainsKey(type) == false) return;

            List<MessageItem> items = handles[type];

            MessageItem it = null;
            for (int i = 0, len = items.Count; i < len; i++) {
                it = items[i];
                if (it.reciver == reciver)
                {
                    it.handle -= handle;
                    break;
                }
            }


            if (it == null || it.handle == null) {
                items.Remove(it);
            }
        }

        public void sendMessage(string type) {
            sendMessage(null, type);
        }

        public void sendMessage(object sender, string type) {
            if (handles.ContainsKey(type) == false) return;

            List<MessageItem> items = handles[type];

            MessageItem it = null;
            for (int i = 0, len = items.Count; i < len; i++)
            {
                it = items[i];
                it.handle(sender,null);
            }
        }

        public void sendMessage(object sender, string type, MsgArgs e)
        {
            if (handles.ContainsKey(type) == false) return;
            
            List<MessageItem> items = handles[type];

            MessageItem it = null;
            for (int i = 0, len = items.Count; i < len; i++)
            {
                it = items[i];
                it.handle(sender, e);
            }

        }
        public void sendMessage(object sender, string type,params object[] param)
        {
            if (handles.ContainsKey(type) == false) return;

            List<MessageItem> items = handles[type];

            MsgArgs msgArgs = new MsgArgs();
            msgArgs.paramList = new List<object>(param);

            MessageItem it = null;
            for (int i = 0, len = items.Count; i < len; i++) {
                it = items[i];
                it.handle(sender, msgArgs);
            }

        }

    }

}
