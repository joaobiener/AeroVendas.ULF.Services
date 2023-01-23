To create an environment variable, we have to open the cmd window as 
an administrator and type the following command:

setx SECRET "UnimedLFSecretKey" /M

This is going to create a system environment variable with the name 
SECRET and the value UnimedLFSecretKey. By using /M we specify that 
we want a system variable and not local.