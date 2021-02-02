.SILENT:
.PHONY: release
all: clean build run

clean:
	rm -rf src/bin
	rm -rf src/obj
	rm -rf release*
	rm -rf release*.zip

build:
	dotnet build src

run:
	dotnet run --project src

release:
	make clean
	$(eval version = $(shell cat "src\\nightlight.csproj" | grep -o "<Version>.*</Version>" | grep -o ">.*<" | sed "s/<//;s/>//"))
	@echo ------------------------
	@echo Deploying as v$(version)
	@echo ------------------------
	mkdir -p release-$(version)
	dotnet build src -c Release -o release-$(version)
	dotnet mage -al nightlight.exe -td release-$(version)
	dotnet mage -n Application -t "release-$(version)\\nightlight.manifest" -fd release-$(version) -v $(version)
	dotnet mage -n Deployment -t "release-$(version)\\nightlight.application" -appm "release-$(version)\\nightlight.manifest" -i true -pub "Nightlight" -v $(version)
	echo ".\nightlight.application" > "release-$(version)\\RUN_ME.bat"
	tar -cf release-$(version).tar release-$(version)