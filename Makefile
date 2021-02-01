.PHONY: release
all: clean build run

clean:
	rm -rf bin
	rm -rf obj
	rm -rf release

build:
	dotnet build

run:
	dotnet run

build-release:
	mkdir -p release
	dotnet build -c Release -o release

release:
ifndef v
	$(error Version undefined. Example: "make release v=1.0.0.0")
endif
	make clean
	make build-release
	dotnet mage -al nightlight.exe -td release
	dotnet mage -new Application -t "release\\nightlight.manifest" -fd release -v $(v)
	dotnet mage -new Deployment -Install true -pub "abc def" -v $(v) -AppManifest "release\\nightlight.manifest" -t nightlight.application