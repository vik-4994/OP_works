//const Comment = require('../models/comment.model')
const User = require('../models/user.model')
const bcrypt = require('bcrypt-nodejs')

module.exports.changePassword = async (req, res) => {
    const candidate = await User.findOne({login: req.body.login})
    const isPasswordCorrect = bcrypt.compareSync(req.body.oldPassword, candidate.password)
    if(isPasswordCorrect){
        try {
            const salt = bcrypt.genSaltSync(10)
            const $set = {
                password: bcrypt.hashSync(req.body.password, salt)
            }
            const user = await User.findOneAndUpdate({login: req.body.login}, {$set}, {new: true})
            res.json(user)
        } catch (error) {
            res.status(500).json(error)
        }
    } else {
        res.status(409).json({message: 'Старый пароль неверный'})
    }
}

module.exports.getInfo = async (req, res) => {
    try {
        const user = await User.findOne({login: req.body.login})
        res.json(user)
    } catch (error) {
        res.status(500).json(error)
    }
}