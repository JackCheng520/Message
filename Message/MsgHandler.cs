using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Assets.JackCheng.GameRemind
{
    [Serializable]
    [ComVisible(true)]
    public delegate void MsgHandler(object sender, MsgArgs e);

    [Serializable]
    [ComVisible(true)]
    public class MsgArgs
    {
        public static readonly MsgArgs Empty;

        public List<object> paramList = null;
    }
}
