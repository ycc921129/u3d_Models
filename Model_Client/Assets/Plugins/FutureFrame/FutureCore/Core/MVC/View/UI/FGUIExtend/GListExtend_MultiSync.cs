/****************************************************************************
* ScriptType: Ö÷¿ò¼Ü
* ÇëÎðÐÞ¸Ä!!!
****************************************************************************/

using FairyGUI;
using System.Collections.Generic;

namespace FutureCore.FGUIExtend
{
    public class GListExtend_MultiSync
    {
        private List<GList> glist_syncs;
        private List<ListItemRenderer> glist_sync_cbs;

        public void Init(List<GList> glist_syncs, List<ListItemRenderer> glist_sync_cbs, int numItems)
        {
            this.glist_syncs = glist_syncs;
            this.glist_sync_cbs = glist_sync_cbs;
            GList gList_main = glist_syncs[0];

            gList_main.scrollPane.onScroll.Add(() =>
            {
                for (int i = 1; i < this.glist_syncs.Count; i++)
                {
                    GList gList = this.glist_syncs[i];
                    gList.container.x = gList_main.container.x;
                }
            });

            for (int i = 0; i < this.glist_syncs.Count; i++)
            {
                GList gList = this.glist_syncs[i];
                ListItemRenderer cb = this.glist_sync_cbs[i];
                gList.itemRenderer = cb;
            }

            for (int i = 0; i < this.glist_syncs.Count; i++)
            {
                GList gList = this.glist_syncs[i];
                gList.numItems = numItems;
            }
        }

        public void Dispose()
        {
            glist_syncs.Clear();
            glist_syncs = null;

            glist_sync_cbs.Clear();
            glist_sync_cbs = null;
        }
    }
}