using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region Get_PBIpBlocks
    [Cmdlet(VerbsCommon.Get, "PBIpBlocks")]
    public class Get_PBIpBlocks : PBapiPSCmdlet
    {
        protected override void ProcessRecord()
        {
            this.WriteObjects(PBApi.Service.getAllPublicIpBlocks());
        }
    }
    #endregion

    #region New-PBIpBlock
    [Cmdlet(VerbsCommon.New, "PBIpBlock")]
    public class New_PBIpBlock : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public int blockSize;

        [Parameter(
            Position = 1,
            Mandatory = false,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public location Location;

        protected override void ProcessRecord()
        {
            createReservePublicIpBlockRequest Request = new createReservePublicIpBlockRequest();
            Request.blockSize = blockSize;
            Request.location = Location;

            this.WriteObject(PBApi.Service.reservePublicIpBlock(Request));
        }
    }
    #endregion

    #region Add-PBIpToNic
    [Cmdlet(VerbsCommon.Add, "PBIpToNic")]
    public class Add_PBIpToNic : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string ip;

        [Parameter(
            Position = 1,
            Mandatory = false,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string nicId;

        protected override void ProcessRecord()
        {
            PBapiChecks.IsIP(ip);
            this.WriteObject(PBApi.Service.addPublicIpToNic(ip,nicId));
        }
    }
    #endregion

    #region Remove-PBIpFromNic
    [Cmdlet(VerbsCommon.Remove, "PBIpFromNic")]
    public class Remove_PBIpFromNic : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string ip;

        [Parameter(
            Position = 1,
            Mandatory = false,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string nicId;

        protected override void ProcessRecord()
        {
            PBapiChecks.IsIP(ip);
            this.WriteObject(PBApi.Service.removePublicIpFromNic(ip, nicId));
        }
    }
    #endregion

    #region Remove-PBIpBlock
    [Cmdlet(VerbsCommon.Remove, "PBIpBlock")]
    public class Remove_PBIpBlock : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string blockId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.releasePublicIpBlock(blockId));
        }
    }
    #endregion

}
