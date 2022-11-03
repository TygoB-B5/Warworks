// Fill out your copyright notice in the Description page of Project Settings.


#include "PlayableGrid.h"
#include "DrawDebugHelpers.h"

// Sets default values
APlayableGrid::APlayableGrid()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;
}

// Called when the game starts or when spawned
void APlayableGrid::BeginPlay()
{
	Super::BeginPlay();
	InvalidatePositionData();
	InvalidateColorMeshData();
}

void APlayableGrid::InvalidatePositionData()
{

	// Initilize elements.
	uint32_t size = GridSize.X * GridSize.Y;
	m_Positions.Init(FVector2D(), size);

	// Fill arrays.
	for (size_t i = 0; i < size; i++)
	{
		uint32_t x = i % GridSize.X;
		uint32_t y = i / GridSize.Y;

		m_Positions[i] = FVector2D(x, y) * PositionOffset;
	}
}

void APlayableGrid::InvalidateColorMeshData()
{
	m_Tiles.Init(nullptr, m_Positions.Num());

	for (size_t i = 0; i < m_Positions.Num(); i++)
	{
		if (Tile)
		{
			m_Tiles[i] = GetWorld()->SpawnActor<ATile>(Tile);
			Cast<AActor>(m_Tiles[i])->AddActorLocalOffset(FVector(m_Positions[i], GridSize.Z));
		}
	}
}

void APlayableGrid::DrawDebugHelpers()
{
	// Draw debug spheres.
	for (auto& pos : m_Positions)
	{
		DrawDebugSphere(GetWorld(), FVector(pos, 0), 10, 8, FColor::Red, false, 0.0f);
	}
}

bool APlayableGrid::EditorValuesChanged()
{
	bool changed = GridSize != m_OldGridSize ||
		PositionOffset != m_OldPositionOffset;

	m_OldPositionOffset = PositionOffset;
	m_OldGridSize = GridSize;

	return changed;
}

// Called every frame
void APlayableGrid::Tick(float DeltaTime)
{

	// Update only in editor (Optimization)
	if (GetWorld()->WorldType == EWorldType::Editor)
	{
		if(EditorValuesChanged())
			InvalidatePositionData();

		DrawDebugHelpers();
	}

	Super::Tick(DeltaTime);
}

void APlayableGrid::GetPositionAtCoordinate(const int x, const int y, FVector2D& out)
{
	uint32 index = x + y * GridSize.X;
	out = m_Positions[index];
}

void APlayableGrid::SetTileMaterialIndex(const int x, const int y, const int materialIndex)
{
	int index = x + y * GridSize.X;
	if (index < (GridSize.X * GridSize.Y))
	{
		m_Tiles[index]->SetMaterialIndex(materialIndex);
	}
}

