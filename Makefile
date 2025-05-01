PUBLISHDIR="./PUBLISHED"

publish:
	dotnet publish ./Interpret/Interpret.csproj --runtime linux-x64 --no-self-contained --output $(PUBLISHDIR)

clean:
	rm -rf ./Interpret/bin/Release/net8.0/linux-x64
	rm -rf $(PUBLISHDIR)
