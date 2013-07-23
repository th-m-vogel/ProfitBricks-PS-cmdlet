using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region Get_PBNotifications
    [Cmdlet(VerbsCommon.Get, "PBNotifications")]
    public class Get_Notifications : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string dataCenterId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Servive.getNotifications(dataCenterId));
        }
    }
    #endregion 

    #region Remove_PBNotifications
    [Cmdlet(VerbsCommon.Remove, "PBNotifications")]
    public class Remove_Notifications : PBapiPSCmdlet
    {

        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string[] notificationsId;

        protected override void ProcessRecord()
        {
            PBApi.Servive.deleteNotifications(notificationsId);
        }
    }
    #endregion 
}
