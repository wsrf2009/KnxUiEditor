using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structure.Control
{
    /// <summary>
    /// 定时任务列表
    /// </summary>
    public class KNXTimerTaskListView : KNXControlBase
    {
        public KNXTimer SelectedTimer { get; set; }
    }

    public class KNXTimer
    {
        public KNXTimer(string name, int id)
        {
            this.Name = name;
            this.Id = id;
        }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
