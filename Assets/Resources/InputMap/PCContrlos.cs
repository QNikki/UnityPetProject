// GENERATED AUTOMATICALLY FROM 'Assets/Resources/InputMap/PCContrlos.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PCContrlos : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PCContrlos()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PCContrlos"",
    ""maps"": [
        {
            ""name"": ""PlayerInput"",
            ""id"": ""9a79c1c7-eed9-41c2-86e8-c4591dc88627"",
            ""actions"": [
                {
                    ""name"": ""LeftHand"",
                    ""type"": ""Button"",
                    ""id"": ""84f069c9-cd14-4609-b60e-d36ebc9ecb62"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightHand"",
                    ""type"": ""Button"",
                    ""id"": ""cae28fb7-26bb-4ae9-bd42-e68d2fb885ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""ec033599-347c-4a13-9f7e-d0b949334a89"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability1"",
                    ""type"": ""Button"",
                    ""id"": ""6d6b0dd0-6d2d-4a61-a56e-69d5fc4963ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability2"",
                    ""type"": ""Button"",
                    ""id"": ""99f46d49-6f19-443e-a812-ab8fd454b48f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""afcb0bae-1ae5-4548-93a9-f9823465c178"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""cdcb143d-a02c-4bd1-9c80-6ec0e76a53c9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1e4ff8c3-e917-4bdb-8b10-2d1cb7306410"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInput"",
                    ""action"": ""LeftHand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9463340-1dd8-4212-a9e0-97ee8812913f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInput"",
                    ""action"": ""RightHand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""c0d0c5d1-8fff-4141-baca-3f818af8df7e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8261c48b-da48-4f53-9227-a775fb46b292"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""86ae083a-0fee-419f-9645-b0234a706306"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""db47eafe-4b4e-466f-8c09-49708ba1f284"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ec22292c-2fe9-4d39-851a-095ffb1412c3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""84e82b30-7d2d-45ee-96dc-76d43bb48104"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInput"",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80b56aca-5304-46e1-8dc4-3ec002e72ed9"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInput"",
                    ""action"": ""Ability2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5baed941-510c-4651-9901-62e91dfc0fa9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInput"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b23e9e6-d3b8-46f9-a631-47374f684abd"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PlayerInput"",
            ""bindingGroup"": ""PlayerInput"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerInput
        m_PlayerInput = asset.FindActionMap("PlayerInput", throwIfNotFound: true);
        m_PlayerInput_LeftHand = m_PlayerInput.FindAction("LeftHand", throwIfNotFound: true);
        m_PlayerInput_RightHand = m_PlayerInput.FindAction("RightHand", throwIfNotFound: true);
        m_PlayerInput_Movement = m_PlayerInput.FindAction("Movement", throwIfNotFound: true);
        m_PlayerInput_Ability1 = m_PlayerInput.FindAction("Ability1", throwIfNotFound: true);
        m_PlayerInput_Ability2 = m_PlayerInput.FindAction("Ability2", throwIfNotFound: true);
        m_PlayerInput_Jump = m_PlayerInput.FindAction("Jump", throwIfNotFound: true);
        m_PlayerInput_Look = m_PlayerInput.FindAction("Look", throwIfNotFound: true);
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

    // PlayerInput
    private readonly InputActionMap m_PlayerInput;
    private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
    private readonly InputAction m_PlayerInput_LeftHand;
    private readonly InputAction m_PlayerInput_RightHand;
    private readonly InputAction m_PlayerInput_Movement;
    private readonly InputAction m_PlayerInput_Ability1;
    private readonly InputAction m_PlayerInput_Ability2;
    private readonly InputAction m_PlayerInput_Jump;
    private readonly InputAction m_PlayerInput_Look;
    public struct PlayerInputActions
    {
        private @PCContrlos m_Wrapper;
        public PlayerInputActions(@PCContrlos wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftHand => m_Wrapper.m_PlayerInput_LeftHand;
        public InputAction @RightHand => m_Wrapper.m_PlayerInput_RightHand;
        public InputAction @Movement => m_Wrapper.m_PlayerInput_Movement;
        public InputAction @Ability1 => m_Wrapper.m_PlayerInput_Ability1;
        public InputAction @Ability2 => m_Wrapper.m_PlayerInput_Ability2;
        public InputAction @Jump => m_Wrapper.m_PlayerInput_Jump;
        public InputAction @Look => m_Wrapper.m_PlayerInput_Look;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
            {
                @LeftHand.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLeftHand;
                @LeftHand.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLeftHand;
                @LeftHand.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLeftHand;
                @RightHand.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRightHand;
                @RightHand.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRightHand;
                @RightHand.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRightHand;
                @Movement.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMovement;
                @Ability1.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAbility1;
                @Ability1.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAbility1;
                @Ability1.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAbility1;
                @Ability2.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAbility2;
                @Ability2.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAbility2;
                @Ability2.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAbility2;
                @Jump.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLook;
            }
            m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftHand.started += instance.OnLeftHand;
                @LeftHand.performed += instance.OnLeftHand;
                @LeftHand.canceled += instance.OnLeftHand;
                @RightHand.started += instance.OnRightHand;
                @RightHand.performed += instance.OnRightHand;
                @RightHand.canceled += instance.OnRightHand;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Ability1.started += instance.OnAbility1;
                @Ability1.performed += instance.OnAbility1;
                @Ability1.canceled += instance.OnAbility1;
                @Ability2.started += instance.OnAbility2;
                @Ability2.performed += instance.OnAbility2;
                @Ability2.canceled += instance.OnAbility2;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
            }
        }
    }
    public PlayerInputActions @PlayerInput => new PlayerInputActions(this);
    private int m_PlayerInputSchemeIndex = -1;
    public InputControlScheme PlayerInputScheme
    {
        get
        {
            if (m_PlayerInputSchemeIndex == -1) m_PlayerInputSchemeIndex = asset.FindControlSchemeIndex("PlayerInput");
            return asset.controlSchemes[m_PlayerInputSchemeIndex];
        }
    }
    public interface IPlayerInputActions
    {
        void OnLeftHand(InputAction.CallbackContext context);
        void OnRightHand(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnAbility1(InputAction.CallbackContext context);
        void OnAbility2(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
    }
}
