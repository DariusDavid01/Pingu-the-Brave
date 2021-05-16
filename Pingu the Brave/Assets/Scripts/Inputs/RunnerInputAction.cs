// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/RunnerInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @RunnerInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @RunnerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""RunnerInputAction"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""f846f551-d437-4eb7-bf6a-56bd6bf2903a"",
            ""actions"": [
                {
                    ""name"": ""Tap"",
                    ""type"": ""Button"",
                    ""id"": ""6c19ee6f-a027-4e83-ac5a-dbeb610e0042"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9137c755-7b4a-4f2f-82d4-7335fe8cde58"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StartDrag"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ff63dc31-af8d-4c1e-b9c1-39067d1a4901"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""EndDrag"",
                    ""type"": ""PassThrough"",
                    ""id"": ""563855ae-c453-42dd-951c-95ef8c1d1dcb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6c5079d6-a50e-4d2d-97bb-3235e97cae98"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Computer"",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bd03f66-b5ec-4713-b8d4-eafe47acc0c9"",
                    ""path"": ""<Touchscreen>/touch*/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9bf6a238-ce42-490b-b1d8-030348e18d60"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Computer"",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b5fe2cb-f52b-4293-ae5c-495a2fbd78dd"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfaafd0b-cf87-473e-9702-865debf6abe6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Computer"",
                    ""action"": ""StartDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88b00f83-4de1-4487-8cc4-c13a52e0807a"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""StartDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94209669-1395-42e0-b7c0-bdcf8fed4fb3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Computer"",
                    ""action"": ""EndDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea3073d2-a1f4-4263-8b43-ea7e731f6806"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""EndDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Computer"",
            ""bindingGroup"": ""Computer"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mobile"",
            ""bindingGroup"": ""Mobile"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Tap = m_Gameplay.FindAction("Tap", throwIfNotFound: true);
        m_Gameplay_TouchPosition = m_Gameplay.FindAction("TouchPosition", throwIfNotFound: true);
        m_Gameplay_StartDrag = m_Gameplay.FindAction("StartDrag", throwIfNotFound: true);
        m_Gameplay_EndDrag = m_Gameplay.FindAction("EndDrag", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Tap;
    private readonly InputAction m_Gameplay_TouchPosition;
    private readonly InputAction m_Gameplay_StartDrag;
    private readonly InputAction m_Gameplay_EndDrag;
    public struct GameplayActions
    {
        private @RunnerInputAction m_Wrapper;
        public GameplayActions(@RunnerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Tap => m_Wrapper.m_Gameplay_Tap;
        public InputAction @TouchPosition => m_Wrapper.m_Gameplay_TouchPosition;
        public InputAction @StartDrag => m_Wrapper.m_Gameplay_StartDrag;
        public InputAction @EndDrag => m_Wrapper.m_Gameplay_EndDrag;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Tap.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTap;
                @Tap.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTap;
                @Tap.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTap;
                @TouchPosition.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouchPosition;
                @StartDrag.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStartDrag;
                @StartDrag.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStartDrag;
                @StartDrag.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStartDrag;
                @EndDrag.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndDrag;
                @EndDrag.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndDrag;
                @EndDrag.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndDrag;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Tap.started += instance.OnTap;
                @Tap.performed += instance.OnTap;
                @Tap.canceled += instance.OnTap;
                @TouchPosition.started += instance.OnTouchPosition;
                @TouchPosition.performed += instance.OnTouchPosition;
                @TouchPosition.canceled += instance.OnTouchPosition;
                @StartDrag.started += instance.OnStartDrag;
                @StartDrag.performed += instance.OnStartDrag;
                @StartDrag.canceled += instance.OnStartDrag;
                @EndDrag.started += instance.OnEndDrag;
                @EndDrag.performed += instance.OnEndDrag;
                @EndDrag.canceled += instance.OnEndDrag;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_ComputerSchemeIndex = -1;
    public InputControlScheme ComputerScheme
    {
        get
        {
            if (m_ComputerSchemeIndex == -1) m_ComputerSchemeIndex = asset.FindControlSchemeIndex("Computer");
            return asset.controlSchemes[m_ComputerSchemeIndex];
        }
    }
    private int m_MobileSchemeIndex = -1;
    public InputControlScheme MobileScheme
    {
        get
        {
            if (m_MobileSchemeIndex == -1) m_MobileSchemeIndex = asset.FindControlSchemeIndex("Mobile");
            return asset.controlSchemes[m_MobileSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnTap(InputAction.CallbackContext context);
        void OnTouchPosition(InputAction.CallbackContext context);
        void OnStartDrag(InputAction.CallbackContext context);
        void OnEndDrag(InputAction.CallbackContext context);
    }
}
