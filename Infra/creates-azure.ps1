$webapp = 'goodsstoreapp'
$resourceGroupName = 'rg-goodsstore'
$location = 'northeurope'

New-AzResourceGroup -Name $resourceGroupName -Location $location

New-AzWebAppPlan -ResourceGroupName $resourceGroupName -Location $location -Name $webapp-plan -Tier S1

New-AzWebApp -ResourceGroupName $resourceGroupName -Location $location -Plan $webapp-plan -Name $webapp