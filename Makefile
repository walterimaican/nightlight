all: clean build run

clean:
	rm -rf src/bin
	rm -rf src/obj

build:
	dotnet build src

run:
	dotnet run --project src

publish:
	dotnet publish src -c Release -r win-x64