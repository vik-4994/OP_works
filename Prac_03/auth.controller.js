const bcrypt = require('bcrypt-nodejs')
const jwt = require('jsonwebtoken')
const keys = require('../keys')
const User = require('../models/user.model')

module.exports.login = async function(req, res){
    const candidate = await User.findOne({login: req.body.login});
    if(candidate && candidate.status !== "banned") {
        const isPasswordCorrect = bcrypt.compareSync(req.body.password, candidate.password)
        if(isPasswordCorrect){
            const token = jwt.sign({
                login: candidate.login, userId: candidate._id, role:candidate.role
            }, keys.JWT, {expiresIn: 60*60})
            let role = candidate.role;
            res.json({token, role})
        } else {
            res.status(401).json({message: 'Пароль неверный'})
        }
    } else {
        res.status(404).json({message: 'Пользователь не найден или забанен'})
    }
}

module.exports.createUser = async function(req, res){
    const candidate = await User.findOne({login: req.body.login})

    if (candidate) {
        res.status(409).json({message: "Такой логин уже занят"})
    } else {
        const salt = bcrypt.genSaltSync(10)
        const user = new User({
            login: req.body.login,
            password: bcrypt.hashSync(req.body.password, salt),
            role: req.body.role
        })
        const token = jwt.sign({
            login: req.body.login, userId: user._id, role:req.body.role
        }, keys.JWT, {expiresIn: 60*60})
        let role = req.body.role;
        await user.save()
        res.json({token, role})
    }
}

module.exports.deleteUser = async function(req, res){
    const candidate = await User.findById(req.body.id)

    if(candidate){
        await candidate.deleteOne();
        res.status(202).json(candidate)
    } else {
        res.status(409).json({message: "Такого пользователя нет"})
    }
}