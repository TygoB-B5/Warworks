// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Character.h"
#include "GridMovable.h"
#include "Piece.generated.h"

UENUM(BlueprintType)
enum class ElementType : uint8
{
	Fire = 0    UMETA(DisplayName = "Fire"),
	Earth = 1   UMETA(DisplayName = "Earth"),
	Air = 2     UMETA(DisplayName = "Air"),
	Water = 3   UMETA(DisplayName = "Water")
};	

UCLASS()
class WARWORKS_API APiece : public ACharacter, public IGridMovable
{
	GENERATED_BODY()

public:


public:
	// Sets default values for this character's properties
	APiece();

protected:
	// Called when the game starts or when spawned
	virtual void BeginPlay() override;

public:	
	// Called every frame
	virtual void Tick(float DeltaTime) override;

	// Called to bind functionality to input
	virtual void SetupPlayerInputComponent(class UInputComponent* PlayerInputComponent) override;

public:
	UPROPERTY(EditAnywhere, Category = "Piece")
		ElementType Type;

	UPROPERTY(EditAnywhere, Category = "Piece")
		TArray<bool> AttackPattern;

	UPROPERTY(EditAnywhere, Category = "Piece")
		TArray<bool> WalkPattern;
};
