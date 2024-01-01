import axios from 'axios'

export const SaucesComponent = () => {
    const endpoint = 'http://localhost:5268/sauces';
    const sauces =   await axios.get(endpoint).then()
    return <
}