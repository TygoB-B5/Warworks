// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "Math/IntVector.h"
#include "UObject/UnrealType.h"
#include "Components/StaticMeshComponent.h"
#include "Tile.h"
#include "GridMovable.h"
#include "UObject/ScriptInterface.h"
#include "PlayableGrid.generated.h"

UCLASS()
class WARWORKS_API APlayableGrid : public AActor
{
	GENERATED_BODY()
	
public:	
	APlayableGrid();

protected:
	virtual void BeginPlay() override;
	virtual void Destroyed() override;

private:
	void InvalidatePositionData();
	void InvalidateColorMeshData();
	void InvalidateMovableData();

	void DrawDebugHelpers();
	bool EditorValuesChanged();

public:	
	virtual bool ShouldTickIfViewportsOnly() const override { return true; }
	virtual void Tick(float DeltaTime) override;

	UFUNCTION(BlueprintCallable, Category= "Grid")
		void GetPositionFromCoordinate(const int x, const int y, FVector& out);

	UFUNCTION(BlueprintCallable, Category = "Grid")
		void GetGridMovableFromCoordinate(const int x, const int y, TScriptInterface<IGridMovable>& out);

	UFUNCTION(BlueprintCallable, Category = "Grid")
		void RequestMovementToCoordinateWithCoordinate(const int x, const int y, const int x2, const int y2);

	UFUNCTION(BlueprintCallable, Category = "Grid")
		void RequestMovementToCoordinateWithGridMovable(const int x, const int y, const TScriptInterface<IGridMovable>& movable);

	UFUNCTION(BlueprintCallable, Category = "Grid")
		void SetTileMaterialIndex(const int x, const int y, const int materialIndex);

public:
	UPROPERTY(EditAnywhere, Category = "Grid")
		FIntVector GridSize;

	UPROPERTY(EditAnywhere, Category = "Grid")
		float PositionOffset;

	UPROPERTY(EditAnywhere, Category = "Grid")
		TSubclassOf<class ATile> Tile;

private:
	TArray<FVector> m_Positions;
	TArray<ATile*> m_Tiles;
	TArray<IGridMovable*> m_Movables;

	FVector m_OldActorLocation;
	FIntVector m_OldGridSize;
	float m_OldPositionOffset;
};
