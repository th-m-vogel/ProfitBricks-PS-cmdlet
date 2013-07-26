using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region Connect_PBRomdriveToServer
    [Cmdlet("Connect", "PBRomdriveToServer")]
    public class Connect_PBRomdriveToServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string imageId;

        [Parameter(
            Position = 1,
            Mandatory = true
        )]
        public string serverId;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public int deviceNumber;

        protected override void ProcessRecord()
        {
            romDriveRequest Request = new romDriveRequest();
            Request.imageId = imageId;
            Request.serverId = serverId;
            Request.deviceNumber = deviceNumber;
            if (deviceNumber != 0)
            {
                Request.deviceNumberSpecified = true;
            }  
            this.WriteObject(PBApi.Service.addRomDriveToServer(Request));
        }
    }
    #endregion

    #region Remove_PBRomdriveFromServer
    [Cmdlet(VerbsCommon.Remove, "PBRomdriveFromServer")]
    public class Remove_PBRomdriveFromServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string imageId;

        [Parameter(
            Position = 1,
            Mandatory = true
        )]
        public string serverId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.removeRomDriveFromServer(imageId, serverId));
        }
    }
    #endregion

}
