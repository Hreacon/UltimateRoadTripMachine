while(1) {
  clear
  Write-Host "Running Test"
  dnx test
  start-sleep -s 3
  Write-Host "Running Test"
  dnx test
  start-sleep -s 3
}
