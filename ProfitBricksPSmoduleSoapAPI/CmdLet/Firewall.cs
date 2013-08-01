using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region New_PBFirewallRules
    [Cmdlet(VerbsCommon.New, "PBFirewallRules")]
    public class New_PBFirewallRules : PBapiPSCmdlet
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
        public firewallRuleRequest[] firewallRuleRequest;

        protected override void ProcessRecord()
        {
            // not implemented yet
            // PBapiChecks.IsFWrule(request)
            this.WriteObject(PBApi.Service.addFirewallRulesToNic(firewallRuleRequest, nicId));
        }
    }
    #endregion

    #region New_PBFirewallRule
    [Cmdlet(VerbsCommon.New, "PBFirewallRule")]
    public class New_PBFirewallRule : PBapiPSCmdlet
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
        public string firewallRuleName;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public protocol? protocol;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public string sourceMac;

        [Parameter(
            Position = 4,
            Mandatory = false
        )]
        public string sourceIp;

        [Parameter(
            Position = 5,
            Mandatory = false
        )]
        public string targetIp;

        [Parameter(
            Position = 6,
            Mandatory = false
        )]
        public int? portRangeStart;

        [Parameter(
            Position = 7,
            Mandatory = false
        )]
        public int? portRangeEnd;

        [Parameter(
            Position = 8,
            Mandatory = false
        )]
        public int? icmpType;

        [Parameter(
            Position = 9,
            Mandatory = false
        )]
        public int? icmpCode;

        protected override void ProcessRecord()
        {
            firewallRuleRequest[] request = { new firewallRuleRequest() }; 
            if (
                !protocol.HasValue &&
                string.IsNullOrEmpty(sourceMac) &&
                string.IsNullOrEmpty(sourceIp) &&
                string.IsNullOrEmpty(targetIp) &&
                !portRangeStart.HasValue &&
                !portRangeEnd.HasValue &&
                !icmpType.HasValue &&
                !icmpCode.HasValue
                )
            {
                throw new System.ArgumentException("at leat on of the following parameters must have a valid value: protocol, sourceMac, sourceIp, targetIp, portRangeStart, portRangeEnd, icmpType, icmpCode");
            }
            if (!string.IsNullOrEmpty(sourceMac))
            {
                PBapiChecks.IsMAC(sourceMac);
            }
            if (!string.IsNullOrEmpty(sourceIp))
            {
                PBapiChecks.IsIP(sourceIp);
            }
            if (!string.IsNullOrEmpty(targetIp))
            {
                PBapiChecks.IsIP(targetIp);
            }

            if (((int)portRangeStart > 65534) || ((int)portRangeEnd > 65534))
            {
                throw new System.ArgumentException("maximum allowed value for portRangeStart and portRangeEnd is 65534");
            }
            if (icmpCode.HasValue) 
            {
                if ((int)icmpCode > 255)
                {
                    throw new System.ArgumentException("maximum allowed value for icmpCode is 255");
                }    
                request[0].icmpCode = (int)icmpCode;
                request[0].icmpCodeSpecified = true;
            }
            if (icmpType.HasValue)
            {
                if ((int)icmpType > 255)
                {
                    throw new System.ArgumentException("maximum allowed value for icmpType is 255");
                }    
                request[0].icmpType = (int)icmpType; 
                request[0].icmpTypeSpecified = true;
            }
            if (protocol.HasValue)
            {
                request[0].protocol = (protocol)protocol;
                request[0].protocolSpecified = true;
            }
            if (((int)portRangeEnd > 0) || ((int)portRangeStart > 0))
            {
                WriteObject(string.Compare(request[0].protocol.ToString(), "TCP") );
                if (request[0].protocolSpecified && string.Compare(request[0].protocol.ToString(), "TCP") != 0 && string.Compare(request[0].protocol.ToString(), "UDP") != 0)
                {
                    throw new System.ArgumentException("if portRange[End,Start] is specified, protocol must be TCP or UDP");
                }
                if (((int)portRangeEnd == 0) || ((int)portRangeStart == 0))
                {
                    throw new System.ArgumentException("if portRange[End,Start] is specified, portRange[Start,End] must have a value from 1 to 65534 also");
                }
                if ((int)portRangeEnd < (int)portRangeStart)
                {
                    throw new System.ArgumentException("portRangeEnd less than portRangeStart is not allowed");
                }
                request[0].portRangeEnd = (int)portRangeEnd;
                request[0].portRangeEndSpecified = true;
                request[0].portRangeStart = (int)portRangeStart;
                request[0].portRangeStartSpecified = true;
            }
            request[0].sourceIp = sourceIp;
            request[0].targetIp = targetIp;
            request[0].sourceMac = sourceMac;
            // not implemented yet in wsdl
            // request[0].firewallRuleName = firewallRuleName;
            
            // not implemented yet
            // PBapiChecks.IsFWrule(request)
            this.WriteObject(PBApi.Service.addFirewallRulesToNic(request, nicId));
        }
    }
    #endregion

    #region Get_PBFirewall
    [Cmdlet(VerbsCommon.Get, "PBFirewall")]
    public class Get_PBFirewall : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string firewallId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.getFirewall(firewallId));
        }
    }
    #endregion

    #region Remove_PBFirewallRules
    [Cmdlet(VerbsCommon.Remove, "PBFirewallRules")]
    public class Remove_PBFirewallRules : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string[] firewallRuleIds;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.removeFirewallRules(firewallRuleIds));
        }
    }
    #endregion

    #region Update_PBFirewallStatus
    [Cmdlet(VerbsData.Update, "PBFirewallStatus")]
    public class Update_PBFirewallStatus : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string[] firewallIds;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.activateFirewalls(firewallIds));
        }
    }
    #endregion

    #region Remove_PBFirewalls
    [Cmdlet(VerbsCommon.Remove, "PBFirewalls")]
    public class Remove_PBFirewalls : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string[] firewallIds;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.deleteFirewalls(firewallIds));
        }
    }
    #endregion


}
