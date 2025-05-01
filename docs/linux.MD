# Build & Run Yappembler on Linux

Tested on Linux Mint 21.3 Xfce Edition __x64__.

__1.__ Install .NET 8 SDK

```sh
sudo apt install dotnet-sdk-8.0
```

Doesn't work on your distro? Check out [Package manager instructions](https://learn.microsoft.com/en-us/dotnet/core/install/linux) on Microsoft Learn.

__2.__ Clone the repository and navigate to its root folder

```sh
git clone https://github.com/radekmocek/prk
cd prk
```

__3.__ Run the Makefile with prepared `dotnet publish` command

```sh
make
```

__4.__ Project was published into `PUBLISHED` directory, go there and check if Yappembler is working

```sh
cd PUBLISHED
```

Run a program:

```sh
./Interpret run ../tests/_program.yappp
```

Run a test:

```sh
./Interpret test ../tests/01_print.yappt
```

Run all tests in a directory:

```sh
./Interpret testdir ../tests/
```

What next?

* Write your first Yappembler code! Read the [Yappembler language specification](./reference.MD) and [Examples of code](./examples.MD).
* Test your Yappembler code! Read [Yappembler Test Suite specification](./reference_testing.MD).
* Increase your productivity! Install the [VSCode syntax highlight extension](../Highlighter/yappembler/).
