# Find the Windows Server 2012 Image in Europe Dcatacenter
#$hdd_image = Get-PBImages | where {$_.imagename -like "windows-2012-server*" -and $_.region -eq "EUROPE"}
# create a new and empty Datacenter in Europe
#$new_dc = New-PBDatacenter -dataCenterName "My New datacenter" -Region EUROPE
# create a new 30 Gig Storage based on the above selected Image
#$new_disc = New-PBStorage -size 30 -dataCenterId $new_dc.dataCenterId -storageName "Disk1" -mountImageId $hdd_image.imageId -profitBricksImagePassword "ExtremeSecret"
# Create a Server
#    2 Cores
#    2 Gig RAM
#      using the above created Disk
#      connected to the Intrenet using LanId 1
#$new_server = New-PBServer -cores 2 -ram 2048 -serverName "Win2012-01" -dataCenterId $new_dc.dataCenterId -lanId 1 -internetAccess $true -bootFromStorageId $new_disc.storageId -osType Windows
# done so far, just wait antil provisioning finished
write-host -NoNewline "Wait for provisioning to finish ..."
do {
        write-host -NoNewline "." 
        start-sleep -s 10
} while ((Get-PBDatacenterState -dataCenterId $new_dc.dataCenterId) -ne "AVAILABLE")
# get server information after provisioning
$new_server = Get-PBServer -serverId $new_server.serverId
# give the new server nic a friendly name
$new_nic = Set-PBNic -nicId $new_server.nics[0] -nicName "LAN 1"
# print the nic ip number
Write-host "Primary IP is: "$new_server.nics[0].Ips[0]
# Datacenter is ready
Write-Host "Your new Datacenter is ready for Use."
Write-Host "It may take additional time for your server to boot for the 1st time!"
# done