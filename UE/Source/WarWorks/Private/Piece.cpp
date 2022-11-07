// Fill out your copyright notice in the Description page of Project Settings.


#include "Piece.h"

// Sets default values
APiece::APiece()
{
	PrimaryActorTick.bCanEverTick = true;

	uint32 size = FMath::Pow(PatternSize, 2);

	// Initialize pattern arrays to required length.
	AttackPattern.Init(false, size);
	WalkPattern.Init(false, size);
}

void APiece::BeginPlay()
{
	Super::BeginPlay();
	
}

void APiece::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

}

void APiece::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);
}

