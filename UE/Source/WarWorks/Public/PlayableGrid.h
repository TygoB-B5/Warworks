// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "Math/IntVector.h"
#include "UObject/UnrealType.h"
#include "Components/StaticMeshComponent.h"
#include "Tile.h"
#include "PlayableGrid.generated.h"

UCLASS()
class WARWORKS_API APlayableGrid : public AActor
{
	GENERATED_BODY()
	
public:	
	APlayableGrid();

protected:
	virtual void BeginPlay() override;

private:
	void InvalidatePositionData();
	void InvalidateColorMeshData();

	void DrawDebugHelpers();
	bool EditorValuesChanged();

public:	
	virtual bool ShouldTickIfViewportsOnly() const override { return true; }
	virtual void Tick(float DeltaTime) override;

	UFUNCTION(BlueprintCallable, Category="Grid")
		void GetPositionAtCoordinate(const int x, const int y, FVector2D& out);

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
	TArray<FVector2D> m_Positions;
	TArray<ATile*> m_Tiles;

	FIntVector m_OldGridSize;
	float m_OldPositionOffset;
};
