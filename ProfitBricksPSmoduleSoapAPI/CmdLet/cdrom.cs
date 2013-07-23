using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region Join_PBRomdriveConnection
    [Cmdlet(VerbsCommon.Join, "PBRomdriveConnection")]
    public class Join_PBRomdriveConnection : PBapiPSCmdlet
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
            this.WriteObject(PBApi.Servive.addRomDriveToServer(Request));
        }
    }
    #endregion

    #region Remove_PBRomdriveConnection
    [Cmdlet(VerbsCommon.Remove, "PBRomdriveConnection")]
    public class Remove_PBRomdriveConnection : PBapiPSCmdlet
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
            this.WriteObject(PBApi.Servive.removeRomDriveFromServer(imageId, serverId));
        }
    }
    #endregion

}
