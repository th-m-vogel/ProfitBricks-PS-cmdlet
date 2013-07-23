using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region Get_PBImages
    [Cmdlet(VerbsCommon.Get, "PBImages")]
    public class Get_PBImages : PBapiPSCmdlet
    {
        protected override void ProcessRecord()
        {
            this.WriteObjects(PBApiServive.Servive.getAllImages());
        }
    }
    #endregion

    #region Get_PBImage
    [Cmdlet(VerbsCommon.Get, "PBImage")]
    public class Get_PBImage : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string imageId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApiServive.Servive.getImage(imageId));
        }
    }
    #endregion

    #region Set_PBImage
    [Cmdlet(VerbsCommon.Set, "PBImage")]
    public class Set_PBImage : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string imageId;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public osType osType;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApiServive.Servive.setImageOsType(imageId, osType));
        }
    }
    #endregion
}
