# Open with Administrator a new Developer Command Prompt for VS2019 and point it to the current folder

# Run the following command which says C# compiler please compile MainClass.cs and give me back an executable:

csc MainClass.cs

# Run the following command which says C# compiler please compile MainClass.cs and give me back a dll file

csc /target:library MainClass.cs

# Run the following command

ildasm /out:MyExeCode.txt MainClass.exe

# Run the following command

ildasm /out:MyDllCode.txt MainClass.dll

# Compare the two files