# Build & Run Yappembler on Windows

Tested on Windows 10 Pro 19045.5796 __x64__.

__1.__ Download and install .NET 8 SDK from <https://dotnet.microsoft.com/en-us/download/dotnet/8.0>

__2.__ Open PowerShell and clone the repository and navigate to its root folder

```ps1
git clone https://github.com/radekmocek/prk
cd prk
```

__3.__ Run the Makew script with prepared `dotnet publish` command

```ps1
.\Makew.ps1
```

__4.__ Project was published into `PUBLISHED` directory, go there and check if Yappembler is working

```ps1
cd PUBLISHED
```

Run a program:

```ps1
.\Interpret.exe run ..\tests\_program.yappp
```

Run a test:

```ps1
.\Interpret.exe test ..\tests\01_print.yappt
```

Run all tests in a directory:

```ps1
.\Interpret.exe testdir ..\tests\
```

What next?

* Write your first Yappembler code! Read the [Yappembler language specification](./reference.MD) and [Examples of code](./examples.MD).
* Test your Yappembler code! Read [Yappembler Test Suite specification](./reference_testing.MD).
* Increase your productivity! Install the [VSCode syntax highlight extension](../Highlighter/yappembler/).
