[简体中文](https://github.com/Wilson520403/SafeTicks/blob/main/README%20-%20Ch.md)

## Overview

In typical scenarios, time is perceived as always moving forward. However, in practical production environments, system time may unexpectedly jump backward due to user misoperation or other reasons. This "time rollback" phenomenon can lead to serious issues like data corruption and ID duplication .

Instead of immediately synchronizing timestamps with the potentially unstable system time, the SafeTimestamp solution advances safe timestamps incrementally based on the current system time progression, ensuring temporal consistency and preventing anomalies caused by time reversals .

## Core Components

### SafeTimestamp

A secure timestamp class designed to prevent client-side time rollbacks, maintaining reliable temporal sequencing even when system time becomes unreliable.

### MgrTicks

The timestamp manager that orchestrates timestamp operations and ensures the integrity of temporal progression across the application.

### MgrTicksEnv

The runtime environment for timestamps, supporting different time contexts such as local time and server time, providing flexibility for various application needs.

## Getting Started

### Initializing with Local Environment Time

To begin using the system with local environment time, initialize it with the current system timestamp:

```
MgrTicks.local.Init(MgrTicks.ParseTicksToTimestampMillisecond(DateTime.Now.Ticks));
```

This initialization process converts the system's current time into a reliable timestamp format that the SafeTimestamp system can manage .

### Enabling Event Emission

After initializing with the environment timestamp, enable event emission by calling:

```
MgrTicks.local.OnEmitAble();
```

This step activates the timer event system, allowing the timestamp manager to begin broadcasting temporal events throughout the application.

### Frame-by-Frame Updates

For continuous time management, call the update method in your MonoBehaviour's Update function:

```
MgrTicks.local.OnUpdate();
```

This ensures the timestamp system processes time increments consistently with your application's frame rate, maintaining accurate temporal progression .

## Demonstration

A complete sample scene is available at `Assets\Sample\Scenes\SampleScene`demonstrating the implementation and usage of the SafeTimestamp system in a practical context.

This approach provides a robust solution for maintaining temporal integrity in applications where system time reliability cannot be guaranteed, particularly important in game development, financial applications, and any system requiring consistent temporal sequencing .
