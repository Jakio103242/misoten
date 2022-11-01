using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

namespace Game.Input {
    [CreateAssetMenu(fileName = "InputReader", menuName = "GameSO/Input Reader")]
    public class InputReader : ScriptableObject, GameInputs.IPlayerActions {
        private GameInputs input;
        public GameInputs GameInputs => input;

        // player
        private readonly ReactiveProperty<Vector2> move = new ReactiveProperty<Vector2>(Vector2.zero);
        private readonly ReactiveProperty<Vector2> look = new ReactiveProperty<Vector2>(Vector2.zero);
        private readonly Subject<Unit> onTalk = new Subject<Unit>();
        private readonly Subject<Unit> onDash = new Subject<Unit>();
        private readonly Subject<Unit> onWalk = new Subject<Unit>();

        public IObservable<Vector2> OnMove => move;
        public IObservable<Vector2> OnLook => look;
        public IObservable<Unit> OnTalk => onTalk;
        public IObservable<Unit> OnDash => onDash;
        public IObservable<Unit> OnWalk => onWalk;

        public void EnableGameplayInput() 
        {
            input.Player.Enable();
        }

        public void EnableMenuInput() 
        {
            input.Player.Disable();
        }

        private void OnEnable() 
        {
            if(input == null) 
            {
                input = new GameInputs();
                input.Player.SetCallbacks(this);
            }
            EnableGameplayInput();
        }

        private void OnDisable() 
        {
            input.Disable();
        }

        void GameInputs.IPlayerActions.OnMove(InputAction.CallbackContext ctx) 
        {
            move.Value = ctx.ReadValue<Vector2>();
        }

        void GameInputs.IPlayerActions.OnLook(InputAction.CallbackContext ctx) 
        {
            look.Value = ctx.ReadValue<Vector2>();
        }

        void GameInputs.IPlayerActions.OnTalk(InputAction.CallbackContext ctx) 
        {
            if(ctx.phase == InputActionPhase.Started)
            {
                onTalk.OnNext(Unit.Default);
            }
        }

        void GameInputs.IPlayerActions.OnDash(InputAction.CallbackContext ctx) 
        {
            if(ctx.phase == InputActionPhase.Started)
            {
                onDash.OnNext(Unit.Default);
            }
            else if(ctx.phase == InputActionPhase.Canceled)
            {
                onWalk.OnNext(Unit.Default);
            }
        }
    }
} 
