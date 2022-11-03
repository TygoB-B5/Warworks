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
	InvalidateMovableData();
	InvalidateColorMeshData();
}

void APlayableGrid::InvalidatePositionData()
{

	// Initilize elements.
	uint32_t size = GridSize.X * GridSize.Y;
	m_Positions.Init(FVector(), size);

	// Fill arrays.
	for (size_t i = 0; i < size; i++)
	{
		uint32_t x = i % GridSize.X;
		uint32_t y = i / GridSize.Y;

		FVector2D pos = FVector2D(x, y) * PositionOffset;

		m_Positions[i] = GetActorLocation() + FVector(pos.X, pos.Y, 0);
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
			Cast<AActor>(m_Tiles[i])->AddActorLocalOffset(m_Positions[i]);
		}
	}
}

void APlayableGrid::InvalidateMovableData()
{
	m_Movables.Init(nullptr, GridSize.X * GridSize.Y);
}

void APlayableGrid::DrawDebugHelpers()
{
	// Draw debug spheres.
	for (auto& pos : m_Positions)
	{
		DrawDebugSphere(GetWorld(), pos, 10, 8, FColor::Red, false, 0.0f);
	}
}

bool APlayableGrid::EditorValuesChanged()
{
	bool changed = GridSize != m_OldGridSize ||
		PositionOffset != m_OldPositionOffset ||
		m_OldActorLocation != GetActorLocation();

	m_OldPositionOffset = PositionOffset;
	m_OldGridSize = GridSize;
	m_OldActorLocation = GetActorLocation();

	return changed;
}

// Called every frame
void APlayableGrid::Tick(float DeltaTime)
{

	// Update only in editor (Optimization)
	if (GetWorld()->WorldType == EWorldType::Editor)
	{
		if (EditorValuesChanged())
		{
			InvalidatePositionData();
		}

		DrawDebugHelpers();
	}

	Super::Tick(DeltaTime);
}

void APlayableGrid::GetPositionFromCoordinate(const int x, const int y, FVector& out)
{
	uint32 index = x + y * GridSize.X;

	if (index > (uint32)m_Positions.Num())
	{
		out = FVector();
		return;
	}

	out = m_Positions[index];
}

void APlayableGrid::GetGridMovableFromCoordinate(const int x, const int y, TScriptInterface<IGridMovable>& out)
{
	uint32 index = x + y * GridSize.X;

	if (index > (uint32)m_Positions.Num())
	{
		return;
	}

	out = m_Movables[index]->_getUObject();
}

void APlayableGrid::RequestMovementToCoordinateWithCoordinate(const int x, const int y, const int x2, const int y2)
{
	FVector pos;
	GetPositionFromCoordinate(x, y, pos);

	TScriptInterface<IGridMovable> movable = nullptr;
	GetGridMovableFromCoordinate(x, y, movable);

	if (movable.GetObject())
	{
		IGridMovable::Execute_OnRequestMoveTowards(movable.GetObject(), pos);
	}
}

void APlayableGrid::RequestMovementToCoordinateWithGridMovable(const int x, const int y, const TScriptInterface<IGridMovable>& movable)
{
	FVector pos = FVector();
	GetPositionFromCoordinate(x, y, pos);

	if (movable.GetObject())
	{
		IGridMovable::Execute_OnRequestMoveTowards(movable.GetObject(), pos);
	}
}

void APlayableGrid::SetTileMaterialIndex(const int x, const int y, const int materialIndex)
{
	int index = x + y * GridSize.X;
	if (index < (GridSize.X * GridSize.Y))
	{
		m_Tiles[index]->SetMaterialIndex(materialIndex);
	}
}

