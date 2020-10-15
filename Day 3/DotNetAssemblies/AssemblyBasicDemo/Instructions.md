# Open with Administrator a new Developer Command Prompt for VS2019 and point it to the current folder

# Run the following command which says C# compiler please give me a new library called MyCSharpAssembly.dll from MyCSharpClass.cs:

csc /target:library /out:MyCSharpAssembly.dll MyCSharpClass.cs 

# Run the following command which says Visual Basic compiler please give me a new library called MyVBAssembly.dll from MyVBClass.vb

vbc /target:library /out:MyVBAssembly.dll MyVBClass.vb

# Create a new file called MainClass.cs and put inside the code:

static void Main() 
{
    MyCSharpClass.MyCSharpMethod();
	MyVBClass.MyVBMethod();
}

# In order for the MainClass to see the MyCSharpAssembly.dll and MyVBAssembly.dll, run the following command
# which says C# compiler please add a reference for MyCSharpAssembly.dll and MyVBAssembly.dll to MainClass.cs and generate a MainClass.exe:

csc /reference:MyCSharpAssembly.dll /reference:MyVBAssembly.dll MainClass.cs

# Run MainClass.exe

# Run ildasm (Intermediate Language Disassemble):

ildasm /out:test.txt MyVBAssembly.dll