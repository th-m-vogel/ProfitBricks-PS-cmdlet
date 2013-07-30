using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region New_PBNic
    [Cmdlet(VerbsCommon.New, "PBNic")]
    public class New_PBNic : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string serverId;

        [Parameter(
            Position = 1,
            Mandatory = true
        )]
        public int lanId;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public string ip;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public string nicName;

        protected override void ProcessRecord()
        {
            PBapiChecks.IsIP(ip);
            createNicRequest Request = new createNicRequest();
            Request.serverId = serverId;
            Request.lanId = lanId;
            Request.ip = ip;
            Request.nicName = nicName;
            this.WriteObject(PBApi.Service.createNic(Request));
        }
    }
    #endregion

    #region Get_PBNic
    [Cmdlet(VerbsCommon.Get, "PBNic")]
    public class Get_PBNic : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string nicId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.getNic(nicId));
        }
    }
    #endregion

    #region Remove_PBNic
    [Cmdlet(VerbsCommon.Remove, "PBNic")]
    public class Remove_PBNic : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string nicId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.deleteNic(nicId));
        }
    }
    #endregion

    #region Set_PBNic
    [Cmdlet(VerbsCommon.Set, "PBNic")]
    public class Set_PBNic : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string nicId;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public int lanId;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public string ip;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public string nicName;

        protected override void ProcessRecord()
        {
            updateNicRequest Request = new updateNicRequest();
            if (
                string.IsNullOrWhiteSpace(nicName) &&
                string.IsNullOrWhiteSpace(ip) &&
                lanId == 0
                )
            {
                throw new System.ArgumentException("at leat on of the following parameters must have a valid value: nicName, ip, lanId");
            }
            PBapiChecks.IsIP(ip);
            Request.nicId = nicId;
            Request.lanId = lanId;
            Request.ip = ip;
            Request.nicName = nicName;
            this.WriteObject(PBApi.Service.updateNic(Request));
        }
    }
    #endregion    

    #region Set_PBInternetAccess
    [Cmdlet(VerbsCommon.Set, "PBInternetAccess")]
    public class Set_PBInternetAccess : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string datacenterId;

        [Parameter(
            Position = 1,
            Mandatory = true
        )]
        public int lanId;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public bool internetAccess;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.setInternetAccess(datacenterId, lanId, internetAccess));
        }
    }
    #endregion    

}
