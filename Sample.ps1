	# Find the Windows Server 2012 Image in Europe Dcatacenter
	$hdd_image = Get-PBImages | where {$_.imagename -like "windows-2012-server*" -and $_.region -eq "EUROPE"}
	# create a new and empty Datacenter in Europe
	$new_dc = New-PBDatacenter -dataCenterName "My New datacenter" -Region EUROPE
	# create a new 30 Gig Storage based on the above selected Image
	$new_disc = New-PBStorage -size 30 -dataCenterId $new_dc.dataCenterId -storageName "Disk1" -mountImageId $hdd_image.imageId -profitBricksImagePassword "ExtremeSecret"
	# Create a Server
	#    2 Cores
	#    2 Gig RAM
	#      using the above created Disk
	#      connected to the Intrenet using LanId 1
    $new_server = New-PBServer -cores 2 -ram 2048 -serverName "Win2012-01" -dataCenterId $new_dc.dataCenterId -lanId 1 -internetAccess $true -bootFromStorageId $new_disc.storageId -osType Windows
    