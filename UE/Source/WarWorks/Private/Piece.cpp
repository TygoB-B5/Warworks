// Fill out your copyright notice in the Description page of Project Settings.


#include "Piece.h"

// Sets default values
APiece::APiece()
{
	PrimaryActorTick.bCanEverTick = true;

	// Initialize pattern arrays to required length.
	AttackPattern.Init(false, 5 * 5);
	WalkPattern.Init(false, 5 * 5);
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

