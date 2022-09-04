<CSSTransition
          in = {this.state.show}
          timeout = {1000}
          classNames='fade'
          unmountOnExit
          onEntered={(el)=>{el.style.color='blue'}}
          appear = {true}>
          
        </CSSTransition>