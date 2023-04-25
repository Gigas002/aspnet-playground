# git clean -f -x

# Get all folders named .artifacts or .obj recursively from the current folder
$folders = Get-ChildItem -Path . -Include .artifacts,obj,bin -Recurse -Directory

# Loop through each folder and remove it forcefully
foreach ($folder in $folders) {
  # Get the full path of the folder
  $path = $folder.FullName

  # Remove the folder and all its contents
  Remove-Item -Path $path -Recurse -Force

  # Print a message with the removed directory path
  Write-Host "Removed $path"
}
