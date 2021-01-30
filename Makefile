all: clean build run

clean:
	dotnet clean src

build:
	dotnet build src

run:
	dotnet run --project src