using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region Mount_PBRomdrive
    [Cmdlet(VerbsData.Mount, "PBRomdrive")]
    public class Mount_PBRomdrive : PBapiPSCmdlet
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
        public int? deviceNumber;

        protected override void ProcessRecord()
        {
            romDriveRequest Request = new romDriveRequest();
            Request.imageId = imageId;
            Request.serverId = serverId;
            if (deviceNumber.HasValue)
            {
                Request.deviceNumber = (int)deviceNumber;
                Request.deviceNumberSpecified = true;
            }
 
            this.WriteObject(PBApi.Service.addRomDriveToServer(Request));
        }
    }
    #endregion

    #region Dismount_PBRomdrive
    [Cmdlet(VerbsData.Dismount, "PBRomdrive")]
    public class Dismount_PBRomdrive : PBapiPSCmdlet
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
