# SIMPL# Option – Slot 1 (Proof of Concept)

This repository contains a proof of concept for a basic Crestron control system architecture using SIMPL# Option in Slot 1.

## Components

- **CHtml5 (.CH5z):**  
  Example Crestron HTML5 UI package for TSW/xPanel touch panels.

- **Contract (.cs):**  
  C# contract file that defines the interface between the UI and backend logic.

- **Simpl#Pro (.cpz):**  
  SIMPL# Pro compiled program (backend logic) running on a Crestron control processor.

## Workflow

1. The **CHtml5 (.CH5z)** UI is deployed to a TSW/xPanel or processor.
2. The **Contract (.cs)** defines communication between the UI and backend.
3. The **Simpl#Pro (.cpz)** program runs on the control processor and handles the backend logic.

## Purpose

This project is for demonstration and concept validation only.  
It is not intended for production use.

---

**Note:**  
- This is not a complete solution.  
- Use as a reference for understanding the interaction between CH5 UI, contract files, and SIMPL# Pro logic in a Crestron environment.

