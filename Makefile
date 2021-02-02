.SILENT:
.PHONY: release
all: clean build run

clean:
	rm -rf src/bin
	rm -rf src/obj
	rm -rf release
	rm -rf release*.zip

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
	dotnet mage -n Application -t "release\\nightlight.manifest" -fd release -v $(version)
	dotnet mage -n Deployment -t "release\\nightlight.application" -appm "release\\nightlight.manifest" -i true -pub "Nightlight" -v $(version)
	tar -cf release-$(version).zip release