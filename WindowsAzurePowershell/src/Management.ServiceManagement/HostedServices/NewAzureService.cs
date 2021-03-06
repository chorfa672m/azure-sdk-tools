﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Management.ServiceManagement.HostedServices
{
    using System.Management.Automation;
    using Utilities.Common;
    using WindowsAzure.ServiceManagement;

    /// <summary>
    /// Creates a new hosted service in Windows Azure.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "AzureService", DefaultParameterSetName = "ParameterSetAffinityGroup"), OutputType(typeof(ManagementOperationContext))]
    public class NewAzureServiceCommand : ServiceManagementBaseCmdlet
    {
        public NewAzureServiceCommand()
        {
        }

        public NewAzureServiceCommand(IServiceManagement channel)
        {
            Channel = channel;
        }

        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "ParameterSetAffinityGroup", HelpMessage = "A name for the hosted service that is unique to the subscription.")]
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "ParameterSetLocation", HelpMessage = "A name for the hosted service that is unique to the subscription.")]
        [ValidateNotNullOrEmpty]
        public string ServiceName
        {
            get;
            set;
        }

        [Parameter(Position = 1, Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "ParameterSetLocation", HelpMessage = "Required if AffinityGroup is not specified. The data center region where the clou service will be created.")]
        [ValidateNotNullOrEmpty]
        public string Location
        {
            get;
            set;
        }

        [Parameter(Position = 1, Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "ParameterSetAffinityGroup", HelpMessage = "Required if Location is not specified. The name of an existing affinity group associated with this subscription.")]
        [ValidateNotNullOrEmpty]
        public string AffinityGroup
        {
            get;
            set;
        }

        [Parameter(Position = 2, Mandatory = false, ParameterSetName = "ParameterSetAffinityGroup", HelpMessage = "A label for the cloud service that is Base64-encoded. The label may be up to 100 characters in length. Default: ServiceName.")]
        [Parameter(Position = 2, Mandatory = false, ParameterSetName = "ParameterSetLocation", HelpMessage = "A label for the cloud service that is Base64-encoded. The label may be up to 100 characters in length. Default: ServiceName.")]
        [ValidateNotNullOrEmpty]
        public string Label
        {
            get;
            set;
        }

        [Parameter(Position = 3, Mandatory = false, ValueFromPipelineByPropertyName = true, ParameterSetName = "ParameterSetAffinityGroup", HelpMessage = "A description for the cloud service. The description may be up to 1024 characters in length.")]
        [Parameter(Position = 3, Mandatory = false, ValueFromPipelineByPropertyName = true, ParameterSetName = "ParameterSetLocation", HelpMessage = "A description for the cloud service. The description may be up to 1024 characters in length.")]
        [ValidateNotNullOrEmpty]
        public string Description
        {
            get;
            set;
        }

        public void ExecuteCommand()
        {
            var createHostedServiceInput = new CreateHostedServiceInput
            {
                ServiceName = this.ServiceName,
                Label = string.IsNullOrEmpty(this.Label) ? this.ServiceName : this.Label,
                Description = this.Description,
                AffinityGroup = this.AffinityGroup,
                Location = this.Location
            };

            ExecuteClientActionInOCS(createHostedServiceInput, CommandRuntime.ToString(), s => this.Channel.CreateHostedService(s, createHostedServiceInput));
        }

        protected override void OnProcessRecord()
        {
            this.ExecuteCommand();
        }
    }
}
