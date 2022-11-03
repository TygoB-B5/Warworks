// Fill out your copyright notice in the Description page of Project Settings.


#include "Tile.h"

ATile::ATile()
{
	PrimaryActorTick.bCanEverTick = true;
	Mesh = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Mesh"));

	if (Meshes.Num() > 0)
	{
		Mesh->SetStaticMesh(Meshes[0]);
	}
}

void ATile::SetMaterialIndex(int index)
{
	if (index < Meshes.Num())
	{
		Mesh->SetStaticMesh(Meshes[index]);
	}
}

void ATile::BeginPlay()
{
	Super::BeginPlay();
	
}

void ATile::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

}

