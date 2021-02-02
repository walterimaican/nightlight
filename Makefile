.SILENT:
.PHONY: release
all: clean build run

clean:
	rm -rf src/bin
	rm -rf src/obj
	rm -rf release

build:
	dotnet build src

run:
	dotnet run --project src

build-release:
	mkdir -p release
	dotnet build src -c Release -o release

release:
	make clean
	make build-release
	$(eval version = $(shell cat "src\\nightlight.csproj" | grep -o "<Version>.*</Version>" | grep -o ">.*<" | sed "s/<//;s/>//"))
	@echo ------------------------
	@echo Deploying as v$(version)
	@echo ------------------------
	dotnet mage -al nightlight.exe -td release
	dotnet mage -new Application -t "release\\nightlight.manifest" -fd release -v $(version)
	dotnet mage -new Deployment -Install true -pub "Nightlight" -v $(version) -AppManifest "release\\nightlight.manifest" -t "release\\nightlight.application" -pu "https://github.com/walterimaican/nightlight/blob/release/nightlight.manifest"