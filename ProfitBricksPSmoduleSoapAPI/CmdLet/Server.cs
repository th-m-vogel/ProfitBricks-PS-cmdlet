using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region Get_PBServer
    [Cmdlet(VerbsCommon.Get, "PBServer")]
    public class Get_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string serverId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.getServer(serverId));
        }
    }
    #endregion

    #region New_PBServer
    [Cmdlet(VerbsCommon.New, "PBServer")]
    public class New_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public int cores;

        [Parameter(
            Position = 1,
            Mandatory = true
        )]
        public int ram;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public string dataCenterId;

        [Parameter(
            Position = 4,
            Mandatory = false
        )]
        public string serverName;

        [Parameter(
            Position = 5,
            Mandatory = false
        )]
        public string bootFromImageId;

        [Parameter(
            Position = 6,
            Mandatory = false
        )]
        public string bootFromStorageId;

        [Parameter(
            Position = 7,
            Mandatory = false
        )]
        public int lanId;

        [Parameter(
            Position = 8,
            Mandatory = false
        )]
        public bool internetAccess;

        [Parameter(
            Position = 9,
            Mandatory = false
        )]
        public string availabilityZone;

        [Parameter(
            Position = 10,
            Mandatory = false
        )]
        public string osType;

        protected override void ProcessRecord()
        {
            createServerRequest Request = new createServerRequest();
            Request.cores = cores;
            Request.ram = ram;
            Request.dataCenterId = dataCenterId;
            Request.serverName = serverName;
            Request.bootFromImageId = bootFromImageId;
            Request.bootFromStorageId = bootFromStorageId;
            Request.lanId = lanId ;
            if (lanId != 0)
            {
                Request.lanIdSpecified = true;
            }
            Request.internetAccess = internetAccess;
            // If string value spezified is a valid enum
            // set Request.ParemeterSpecified and Request.Parameter
            if (!(string.IsNullOrEmpty(availabilityZone)))
            {
                if ((Request.availabilityZoneSpecified = Enum.IsDefined(typeof(availabilityZone), availabilityZone.ToUpper())))
                {
                    Request.availabilityZone = (availabilityZone)Enum.Parse(typeof(availabilityZone), availabilityZone.ToUpper());
                }
            }
            if (!(string.IsNullOrEmpty(osType)))
            {
                if ((Request.osTypeSpecified = Enum.IsDefined(typeof(osType), osType.ToUpper())))
                {
                    Request.osType = (osType)Enum.Parse(typeof(osType), osType.ToUpper());
                }
            }

            this.WriteObject(PBApi.Service.createServer(Request));
        }
    }
    #endregion

    #region Reset_PBServer
    [Cmdlet(VerbsCommon.Reset, "PBServer")]
    public class Reset_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string serverId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.resetServer(serverId));
        }
    }
    #endregion

    #region Start_PBServer
    [Cmdlet("Start", "PBServer")]
    public class Start_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string serverId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.startServer(serverId));
        }
    }
    #endregion

    #region Stop_PBServer
    [Cmdlet("Stop", "PBServer")]
    public class Stop_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string serverId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.stopServer(serverId));
        }
    }
    #endregion


    #region Set_PBServer
    [Cmdlet(VerbsCommon.Set, "PBServer")]
    public class Set_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string serverId;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]

        public string serverName;
        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public int cores;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public int ram;

        [Parameter(
            Position = 4,
            Mandatory = false
        )]
        public string bootFromImageId;

        [Parameter(
            Position = 5,
            Mandatory = false
        )]
        public string bootFromStorageId;

        [Parameter(
            Position = 6,
            Mandatory = false
        )]
        public string availabilityZone;

        [Parameter(
            Position = 7,
            Mandatory = false
        )]
        public string osType;

        protected override void ProcessRecord()
        {
            updateServerRequest Request = new updateServerRequest();
            Request.serverId = serverId;
            //  if cores is not 0, cores is a valid value and has to submit in soap request
            Request.cores = cores;
            if (cores != 0)
            {
                Request.coresSpecified = true;
            }
            //  if ram is not 0, ram is a valid value and has to submit in soap request
            Request.ram = ram;
            if (ram != 0)
            {
                Request.ramSpecified = true;
            }
            Request.serverName = serverName;
            Request.bootFromImageId = bootFromImageId;
            Request.bootFromStorageId = bootFromStorageId;
            // If string value spezified is a valid enum
            // set Request.ParemeterSpecified and Parameter
            if (!(string.IsNullOrEmpty(availabilityZone)))
            {
                if ((Request.availabilityZoneSpecified = Enum.IsDefined(typeof(availabilityZone), availabilityZone.ToUpper())))
                {
                    Request.availabilityZone = (availabilityZone)Enum.Parse(typeof(availabilityZone), availabilityZone.ToUpper());
                }
            }
            if (!(string.IsNullOrEmpty(osType)))
            {
                if ((Request.osTypeSpecified = Enum.IsDefined(typeof(osType), osType.ToUpper())))
                {
                    Request.osType = (osType)Enum.Parse(typeof(osType), osType.ToUpper());
                }
            }

            this.WriteObject(PBApi.Service.updateServer(Request));
        }
    }
    #endregion

    #region Remove_PBServer
    [Cmdlet(VerbsCommon.Remove, "PBServer")]
    public class Remove_PBServer : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string serverId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.deleteServer(serverId));
        }
    }
    #endregion

}
