$env:Path += ";C:\Program Files\Git\cmd\;C:\Program Files\Git\bin\";

$scriptpath = $MyInvocation.MyCommand.Path;
$dir = Split-Path $scriptpath;
cd $dir;

$DevelopBranch = "Development";
$CurrentBranch = (git rev-parse --abbrev-ref HEAD) | Out-String;
$Seperator = "----------------------------------";
Write-Output "Current Branch: $CurrentBranch";

Write-Output $Seperator;
Write-Output "Checking out develop";
Write-Output $Seperator;
git checkout $DevelopBranch;

Write-Output $Seperator;
Write-Output "Gathering changes";
Write-Output $Seperator;
git pull;

Write-Output $Seperator;
Write-Output "Returning to $CurrentBranch branch";
Write-Output $Seperator;
git checkout $CurrentBranch.Trim();

Write-Output $Seperator;
Write-Output "Merging changes";
Write-Output $Seperator;
git merge $DevelopBranch;

Start-Sleep -s 10