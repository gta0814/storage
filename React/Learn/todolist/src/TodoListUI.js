import React from 'react';
import { Input, Button, List } from 'antd';

const TodoListUI = (props) => {
    return (
        <div>
            <div style={{ marginTop: '10px', marginLeft: '10px' }}>
                <Input
                    value={props.inputValue} 
                    placeholder="输入"
                    style={{ width: '300px', marginRight: '10px' }}
                    onChange={props.handleInputChange} />
                <Button type="primary" onClick={props.handleBtnClick}>提交</Button>
            </div>
            <List
                style={{ marginTop: '10px', width: '300px' }}
                bordered
                dataSource={props.list}
                renderItem={(item, index) => (<List.Item onClick={(index) => { props.handleItemDelete(index) }}>{item}</List.Item>)}
            />
            {/* <Card title="Card title" extra={<a href="#">More</a>} style={{ width: 300 }}>
          {console.log(this.state.list)}
          <p>{this.state.list[0]}</p>
          <p>{this.state.list[1]}</p>
          {this.state.list.map((index, item) => {
            <p>asdf</p>
          })}
        </Card> */}
        </div>
    )
}

// class TodoListUI extends Component {
//     render() {
//         return (
//             <div>
//                 <div style={{ marginTop: '10px', marginLeft: '10px' }}>
//                     <Input
//                         value={this.props.inputValue}
//                         placeholder="输入"
//                         style={{ width: '300px', marginRight: '10px' }}
//                         onChange={this.props.handleInputChange} />
//                     <Button type="primary" onClick={this.props.handleBtnClick}>提交</Button>
//                 </div>
//                 <List
//                     style={{ marginTop: '10px', width: '300px' }}
//                     bordered
//                     dataSource={this.props.list}
//                     renderItem={(item, index) => (<List.Item onClick={(index) => { this.props.handleItemDelete(index) }}>{item}</List.Item>)}
//                 />
//                 {/* <Card title="Card title" extra={<a href="#">More</a>} style={{ width: 300 }}>
//           {console.log(this.state.list)}
//           <p>{this.state.list[0]}</p>
//           <p>{this.state.list[1]}</p>
//           {this.state.list.map((index, item) => {
//             <p>asdf</p>
//           })}
//         </Card> */}
//             </div>
//         )
//     }
// }

export default TodoListUI;