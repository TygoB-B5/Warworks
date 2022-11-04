// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "UObject/Interface.h"
#include "GridMovable.generated.h"

UINTERFACE(MinimalAPI, Blueprintable)
class UGridMovable : public UInterface
{
	GENERATED_BODY()
};


class WARWORKS_API IGridMovable
{
	GENERATED_BODY()

public:

	UFUNCTION(BlueprintCallable, BlueprintNativeEvent, Category="Grid")
		void OnRequestMoveTowards(const FVector& position);
};
