using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib.DataModels
{
    using Data;
    using Systems;

    public class PushRequest
    {
        private const int listLimit = 100;
        private const int milliseconfDelay = 300;

        private int pushedRecords;

        public event Action<object, PushEventArgs> PushCompleteEvent;

        public PushRequest() { }
        public PushRequest(IEnumerable<object> collection) => PushList = collection;
        public PushRequest(IEnumerable<object> collection, Action<object, PushEventArgs> PushCompleteAction)
        {
            PushList = collection;
            PushCompleteEvent += PushCompleteAction;
        }

        public IEnumerable<object> PushList { get; set; }

        public IEnumerable<IEnumerable<object>> SegmentedList => PushList.Group(listLimit);

        public int Push(Ometria ometria)
        {
            pushedRecords = 0;
            SegmentedList.IteratorDelay(milliseconfDelay).ForEach(col =>
            {
                ometria.Push(DataParser.ObjectToJSon(col));
                pushedRecords += col.ToList().Count;
                OnRaisePushEvent(new PushEventArgs(pushedRecords));
            });
            return pushedRecords;
        }

        protected virtual void OnRaisePushEvent(PushEventArgs e)
        {
            Action<object, PushEventArgs> handler = PushCompleteEvent;
            handler?.Invoke(this, e);
        }
    }

    public class PushEventArgs : EventArgs
    {
        public PushEventArgs(int recordsPushed) => NumberOfRecordsPushed = recordsPushed;
        public int NumberOfRecordsPushed { get; set; }
    }


}
