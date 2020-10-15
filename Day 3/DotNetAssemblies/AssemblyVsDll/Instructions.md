# Open with Administrator a new Developer Command Prompt for VS2019 and point it to the current folder

# Run the following command which says C# compiler please compile Program.cs
# The default behaviour is to target an exe file.
# This command will create a Program.exe file based on Program.cs

csc Program.cs

# Run the following command: 

ildasm /out:test.txt Program.exe

# Open test.txt and examine the .assembly sections and also the .module declaration

# Now we will examine MyFirstModule.cs and MySecondModule.cs files
# We will try to get module files out of them

# Run the following command
# .netmodule is a convention used
# You cannot execute or directly reference .netmodule files. These kind of files are useless unless they are part of an assembly
# But you can still disassemble them with ildasm command and see its MSIL contents.

csc /target:module /out:MyFirstModule.netmodule MyFirstModule.cs

# Run the following command

csc /target:module /out:MySecondModule.netmodule MySecondModule.cs

# Run the following command

csc /addmodule:MyFirstModule.netmodule,MySecondModule.netmodule Program.cs

# Run the following command and examine the multi-file assembly (assembly containing 2 modules) 

ildasm /out:test.txt Program.exe