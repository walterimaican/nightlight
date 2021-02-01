all: clean build run

clean:
	rm -rf bin
	rm -rf obj

build:
	dotnet build

run:
	dotnet run