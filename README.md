## DESCRIPTION:

Power Shell Module to access the ProfitBricks SOAP API

This is a cummunitiy Project not maintained by ProfitBricks

## Usage

Load the Module:

	Import-Module -name "Path_To\ProfitBricksPS.dll" [-verbose]

Initialise the API:

	$var = New-PBApiService [-Username] Username [-Password] Password

Cmdlet usage:

	Verb-PBMethod [-PBApiService] $var {[-Parameters] Value ...}

## LICENSE:

Copyright 2013 Thomas Vogel

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

